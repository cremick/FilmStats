from api_client import APIClient
from web_scraper import LetterboxdScraper
from db_populator import DataIntegrator
from config import BASE_URL, TOKEN

def main():
    api_client = APIClient(BASE_URL, TOKEN)
    scraper = LetterboxdScraper()
    integrator = DataIntegrator(api_client, scraper)
    username = "camrynremick"

    integrator.add_user_films_to_db(username)


if __name__ == "__main__":
    main()