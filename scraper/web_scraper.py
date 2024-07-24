import api_client
import requests
from bs4 import BeautifulSoup
import re
import json
import ast

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
        url = f"https://letterboxd.com/actor/{person_slug}/"
        response = requests.get(url)
        soup = BeautifulSoup(response.text, 'html.parser')

        print(soup)