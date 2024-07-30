import requests
from bs4 import BeautifulSoup
import re
import json
from datetime import datetime

class LetterboxdScraper:
    def get_gender_from_bio(self, bio):
        gender = ""
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

        return gender
    
    def get_birthday_from_bio(self, bio):
        birth_date = datetime.min.date().strftime('%Y-%m-%d')
        patterns = [
            r'born (\w+ \d{1,2}, \d{4})',  # Format: born Month Day, Year
            r'(\w+ \d{1,2}, \d{4}) – ',       # Format: Month Day, Year – (e.g., July 21, 1951 – )
            r'(\w+ \d{1,2}, \d{4}) - ',     # Format: Different hyphen
            r'born (\d{1,2} \w+ \d{4})',    # Format: born Day Month Year (e.g., 15 April 1990)
            r'born (\d{1,2} \w+, \d{4})',    # Format: born Day Month, Year (e.g., 15 April, 1990)
            r'born: (\w+ \d{1,2}, \d{4})',  # Format: born: Month Day, Year
            r'born on (\w+ \d{1,2}, \d{4})', # Format: born on Month Day, Year
            r'born (\d{1,2} \w+ \d{4})',     # Format: born Day Month Year
            r'born [\w\s]+; (\w+ \d{1,2}, \d{4})'    # Format: born [Name]; Month Day, Year
        ]

        for pattern in patterns:
            match = re.search(pattern, bio)
            if match:
                # Extract the date string
                date_str = match.group(1)
                try:
                    if pattern == r'born (\d{1,2} \w+ \d{4})' or pattern == r'born (\d{1,2} \w+ \d{4})':
                        date_obj = datetime.strptime(date_str, '%d %B %Y')
                
                    elif pattern == r'born (\d{1,2} \w+, \d{4})':
                        date_obj = datetime.strptime(date_str, '%d %B, %Y')

                    else:
                        date_obj = datetime.strptime(date_str, '%B %d, %Y')

                    return date_obj.date().strftime('%Y-%m-%d')
                except ValueError:
                    continue

        return birth_date
    
    def get_death_date_from_bio(self, bio):
        death_date = datetime.min.date().strftime('%Y-%m-%d')
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
                death_date = date_obj.date().strftime('%Y-%m-%d')

        return death_date

    def fetch_film_data(self, film_slug):
        url = f"https://letterboxd.com/film/{film_slug}/"
        response = requests.get(url)
        soup = BeautifulSoup(response.text, 'html.parser')

        # Find the script tag containing the filmData variable
        script_tag = soup.find('script', text=re.compile(r'var filmData = \{.*?\};'))
        if script_tag:
            film_data_str = re.search(r'var filmData = (\{.*?\});', script_tag.string).group(1)
            film_data_str = re.sub(r'id: \d+, ', '', film_data_str)
            film_data_str = film_data_str.replace('name:', '"name":').replace('releaseYear:', '"releaseYear":').replace('posterURL:', '"posterURL":').replace('path:', '"path":').replace('runTime:', '"runTime":').replace("\\'", "'")
            film_data = json.loads(film_data_str)
        else:
            return None

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
        else:
            description = ""

        # Tagline
        tagline = soup.find('h4', class_='tagline')
        if tagline:
            tagline = tagline.text.strip()
        else:
            tagline = ""

        # Release Year
        release_year = int(soup.find('div', class_='releaseyear').text.strip())

        # Run time
        run_time = film_data['runTime']

        # Directors
        directors = []
        if 'director' in detailed_film_data:
            directors = [director['sameAs'].split('/')[-2] for director in detailed_film_data['director']]

        # Avg Rating
        average_rating = detailed_film_data.get('aggregateRating', {}).get('ratingValue')
        if not average_rating:
            average_rating = 0

        # Actors
        actors = []
        if 'actors' in detailed_film_data:
            actors = [actor['sameAs'].split('/')[-2] for actor in detailed_film_data['actors']]

        # Genres
        genres = []
        if 'genre' in detailed_film_data:
            genres = detailed_film_data['genre']
            genres = [genre.lower().replace(" ", "-") for genre in genres]

        # Themes
        themes = []
        themes_section = soup.find('h3', string='Themes')
        if themes_section:
            themes_section = themes_section.find_next_sibling('div', class_='text-sluglist')
            themes = [
                (a.text, a['href'].replace("/films/", "").replace("/by/best-match/", ""))
                for a in themes_section.find_all('a', class_='text-slug') 
                if '/films/theme/' in a['href'] or '/films/mini-theme/' in a['href']
            ]

        return {
            "film_data": {
                "title": title,
                "slug": film_slug,
                "releaseYear": release_year,
                "avgRating": average_rating,
                "tagline": tagline,
                "description": description,
                "runTime": run_time
            },
            "actors": actors,
            "directors": directors,
            "genres": genres,
            "themes": themes
        }

    def fetch_person_data(self, person_slug):
        url = f"https://letterboxd.com/producer/{person_slug}/"
        response = requests.get(url)
        soup = BeautifulSoup(response.text, 'html.parser')

        # Name
        h1_tag = soup.find('h1', class_='title-1 prettify')
        if h1_tag:
            goes_by = h1_tag.get_text().replace(h1_tag.span.get_text(), '').strip()
        else:
            return None

        all_names = goes_by.split()
        first_name = all_names[0]

        if len(all_names) == 1:
            last_name = ""
        else:
            last_name = all_names[-1]

        # Get bio
        bio_section = soup.find('section', class_='js-tmdb-bio')
        paragraphs = bio_section.find_all('p')
        bio = ' '.join(paragraph.text for paragraph in paragraphs).lower()

        if bio:
            # Gender
            gender = self.get_gender_from_bio(bio)

            # Birth date
            birth_date = self.get_birthday_from_bio(bio)

            # Death date
            death_date = self.get_death_date_from_bio(bio)

        else:
            birth_date = datetime.min.date().strftime('%Y-%m-%d')
            gender = ""
            death_date = datetime.min.date().strftime('%Y-%m-%d')

        # Get birthday and gender from TMDB if not found in bio
        link = soup.find('a', class_='micro-button')
        url = link['href'] if link else None

        if url:
            tmdb_scraper = TMDBScraper(url)

            if birth_date == datetime.min.date().strftime('%Y-%m-%d'):  
                birth_date = tmdb_scraper.get_birthday()

            if gender == '':
                gender = tmdb_scraper.get_gender()

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
        
        return {
            "firstName": first_name,
            "lastName": last_name,
            "knownAs": goes_by,
            "slug": person_slug,
            "gender": gender,
            "actingCredits": acting_credits,
            "directingCredits": directing_credits,
            "birthDate": birth_date,
            "deathDate": death_date
        }

    def fetch_user_films(self, username):
        url = f"https://letterboxd.com/{username}/films/"

        response = requests.get(url)
        soup = BeautifulSoup(response.text, 'html.parser')

        # Check if user exists
        description_meta = soup.find('meta', attrs={'name': 'description'})
        if not description_meta:
            return None

        # Extract the last page number from the pagination links
        paginate_pages = soup.find('div', class_='paginate-pages')
        if not paginate_pages:
            last_page_number = 1
        else:
            paginate_pages = paginate_pages.find('ul')
            last_page_number = max(int(li.text) for li in paginate_pages.find_all('li') if li.text.isdigit())

        # Loop until the last page
        film_slugs = []
        for page_num in range(1, last_page_number + 1):
            if page_num > 1:
                url = f"https://letterboxd.com/{username}/films/page/{page_num}/"
                response = requests.get(url)
                soup = BeautifulSoup(response.text, 'html.parser')

            # Find all the div elements with class 'film-poster' and extract slugs
            film_posters = soup.find_all('div', class_='film-poster')
            film_slugs = film_slugs + [poster['data-film-slug'] for poster in film_posters if 'data-film-slug' in poster.attrs]

        return film_slugs
    
class TMDBScraper:
    def __init__(self, url):
        response = requests.get(url)
        soup = BeautifulSoup(response.text, 'html.parser')
        
        self.soup = soup

    def get_birthday(self):
        birthday_tag = self.soup.find('strong', text='Birthday').parent
        birthday = birthday_tag.text.split('Birthday')[1].strip()

        pattern = r'\w+ {1,2}\d{1,2}, \d{4}'

        match = re.search(pattern, birthday)
        if match:
            date_str = match.group(0)
            date_obj = datetime.strptime(date_str, '%B %d, %Y')
            birth_date = date_obj.date().strftime('%Y-%m-%d')
            return birth_date

        else:
            return datetime.min.date().strftime('%Y-%m-%d')
        
    def get_gender(self):
        gender_tag = self.soup.find('strong', text='Gender').parent
        gender = gender_tag.text.split('Gender')[1].strip()

        if gender == '-':
            return ""
        else:
            return gender.lower()
