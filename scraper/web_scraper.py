import api_client
import requests
from bs4 import BeautifulSoup
import re
import json
from datetime import datetime

class LetterboxdScraper:
    def __init__(self, api_client):
        self.api_client = api_client

    def fetch_film_data(self, film_slug):
        url = f"https://letterboxd.com/film/{film_slug}/"
        response = requests.get(url)
        soup = BeautifulSoup(response.text, 'html.parser')

        # Find the script tag containing the filmData variable
        script_tag = soup.find('script', text=re.compile(r'var filmData = \{.*?\};'))
        if script_tag:
            film_data_str = re.search(r'var filmData = (\{.*?\});', script_tag.string).group(1)
            film_data_str = film_data_str.replace('id:', '"id":').replace('name:', '"name":').replace('releaseYear:', '"releaseYear":').replace('posterURL:', '"posterURL":').replace('path:', '"path":').replace('runTime:', '"runTime":').replace("\\'", "'")
            film_data = json.loads(film_data_str)
        else:
            print("Movie does not exist")
            return 

        # Find the script tag containing more detailed film data
        script_tag = soup.find('script', type='application/ld+json')
        script_tag = script_tag.text.replace('/* <![CDATA[ */', '').replace('/* ]]> */', '')
        detailed_film_data = json.loads(script_tag)

        # Title
        title = film_data['name']

        # Description
        description = soup.find('meta', property='og:description')
        if description:
            description = description['content']

        # Tagline
        tagline = soup.find('h4', class_='tagline')
        if tagline:
            tagline = tagline.text.strip()

        # Release Year
        release_year = soup.find('div', class_='releaseyear').text.strip()

        # Run time
        run_time = film_data['runTime']

        # Directors
        if 'director' in detailed_film_data:
            directors = [director['sameAs'].split('/')[-2] for director in detailed_film_data['director']]
        else:
            directors = None

        # Avg Rating
        average_rating = detailed_film_data.get('aggregateRating', {}).get('ratingValue')

        # Actors
        if 'actors' in detailed_film_data:
            actors = [actor['sameAs'].split('/')[-2] for actor in detailed_film_data['actors']]
        else:
            actors = None

        # Genres
        if 'genre' in detailed_film_data:
            genres = detailed_film_data['genre']
            genres = [genre.lower().replace(" ", "-") for genre in genres]
        else:
            genres = None

        # Themes
        themes_section = soup.find('h3', string='Themes')
        if themes_section:
            themes_section = themes_section.find_next_sibling('div', class_='text-sluglist')
            themes = [
                a['href'].replace("/films/","").replace("/by/best-match/", "")
                for a in themes_section.find_all('a', class_='text-slug') 
                if '/films/theme/' in a['href'] or '/films/mini-theme/' in a['href']
            ]
        else:
            themes = None

        print("Title:", title)
        print("Slug:", film_slug)
        print("Release Year:", release_year)
        print("Average Rating:", average_rating)
        print("Tagline:", tagline)
        print("Description:", description)
        print("Runtime:", run_time)
        print("Cast:", actors)
        print("Director(s):", directors)
        print("Genres:", genres)
        print("Themes:", themes)

    def fetch_person_data(self, person_slug):
        url = f"https://letterboxd.com/producer/{person_slug}/"
        response = requests.get(url)
        soup = BeautifulSoup(response.text, 'html.parser')

        # Name
        h1_tag = soup.find('h1', class_='title-1 prettify')
        if h1_tag:
            goes_by = h1_tag.get_text().replace(h1_tag.span.get_text(), '').strip()
        else:
            print("Person not found")
            return

        all_names = goes_by.split()
        first_name = all_names[0]

        if len(all_names) == 1:
            last_name = None
        else:
            last_name = all_names[-1]

        # Get bio
        bio_section = soup.find('section', class_='js-tmdb-bio')
        paragraphs = bio_section.find_all('p')
        bio = ' '.join(paragraph.text for paragraph in paragraphs).lower()

        birth_date = None
        gender = None
        death_date = None

        if bio:
            # Gender
            female_count = bio.count(" she ") + bio.count(" her ") + bio.count(" hers ") + bio.count("actress")
            male_count = bio.count(" he ") + bio.count(" him ") + bio.count(" his ") + bio.count("actor")
            nb_count = bio.count(" they ") + bio.count(" their ") + bio.count(" them ") + bio.count("actor")

            counts = {
                'female': female_count,
                'male': male_count,
                'nb': nb_count
            }

            if female_count != 0 or male_count != 0 or nb_count != 0:
                gender = max(counts, key=counts.get)

            # Birthday
            patterns = [
                r'\(born (\w+ \d{1,2}, \d{4})\)',  # Format: born Month Day, Year
                r'(\w+ \d{1,2}, \d{4}) – ',       # Format: Month Day, Year – (e.g., July 21, 1951 – August 11, 2014)
                r'(\w+ \d{1,2}, \d{4}) - ',     # Format: Different hyphen
                r'\(born (\d{1,2} \w+ \d{4})\)',    # Format: born Day Month Year (e.g., 15 April 1990)
                r'\(born (\d{1,2} \w+, \d{4})\)',    # Format: born Day Month, Year (e.g., 15 April, 1990)
                r'\(born: (\w+ \d{1,2}, \d{4})\)',  # Format: born: Month Day, Year
            ]

            for pattern in patterns:
                match = re.search(pattern, bio)
                if match:
                    # Extract the date string
                    date_str = match.group(1)
                    try:
                        if pattern == r'\(born (\d{1,2} \w+ \d{4})\)':
                            date_obj = datetime.strptime(date_str, '%d %B %Y')
                    
                        elif pattern == r'\(born (\d{1,2} \w+, \d{4})\)':
                            date_obj = datetime.strptime(date_str, '%d %B, %Y')

                        else:
                            date_obj = datetime.strptime(date_str, '%B %d, %Y')
    
                        birth_date = date_obj.date()
                    except ValueError:
                        continue

            # Death
            patterns = [
                r'– (\w+ \d{1,2}, \d{4})',
                r'- (\w+ \d{1,2}, \d{4})',
            ]
            for pattern in patterns:
                match = re.search(pattern, bio)
                if match:
                    # Extract the date string
                    death_date_str = match.group(1)
                    date_obj = datetime.strptime(death_date_str, '%B %d, %Y')
                    death_date = date_obj.date()

        # Acting credits
        acting_credits_section = soup.find('a', href=f'/actor/{person_slug}/')
        if acting_credits_section:
            if acting_credits_section.find('small'):
                acting_credits = acting_credits_section.find('small').text
            else:
                acting_credits = 1
        else:
            acting_credits = 0

        # Directing credits
        directing_credits_section = soup.find('a', href=f'/director/{person_slug}/')
        if directing_credits_section:
            if directing_credits_section.find('small'):
                directing_credits = directing_credits_section.find('small').text
            else:
                directing_credits = 1
        else:
            directing_credits = 0

        print("First Name:", first_name)
        print("Last Name:", last_name)
        print("Goes By:", goes_by)
        print("Slug:", person_slug)
        print("Gender:", gender)
        print("Birthday:", birth_date)
        print("Death Date:", death_date)
        print("Acting Credits:", acting_credits)
        print("Directing Credits:", directing_credits)
