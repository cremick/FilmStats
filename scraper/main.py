from api_client import APIClient
from web_scraper import LetterboxdScraper
from db_populator import DataIntegrator
import json
import ast
from config import BASE_URL, TOKEN, TMDB_TOKEN
import time

def main():
    api_client = APIClient(BASE_URL, TOKEN)
    scraper = LetterboxdScraper(TMDB_TOKEN)
    integrator = DataIntegrator(api_client, scraper)
    username = "ninanguyen"

    integrator.add_user_films_to_db(username)
    # print(api_client.get_all("films", True).text)



if __name__ == "__main__":
    main()