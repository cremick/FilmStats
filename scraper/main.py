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
    username = "gwyneth14"

    # print(scraper.fetch_user_films(username))
    # integrator.add_user_films_to_db(username)
    print(scraper.fetch_user_films(username))


if __name__ == "__main__":
    main()