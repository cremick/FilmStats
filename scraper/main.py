from api_client import APIClient
from web_scraper import LetterboxdScraper
from db_populator import add_user_films_to_db
from config import BASE_URL, TOKEN

def main():
    api_client = APIClient(BASE_URL, TOKEN)
    scraper = LetterboxdScraper()
    username = "camrynremick"

    add_user_films_to_db(api_client, scraper, username)

if __name__ == "__main__":
    main()