from api_client import APIClient
from web_scraper import LetterboxdScraper
from config import BASE_URL, TOKEN

def main():
    api_client = APIClient(BASE_URL, TOKEN)
    scraper = LetterboxdScraper(api_client)

    scraper.fetch_person_data("julia-roberts")
    # scraper.fetch_film_data("juno")


    

if __name__ == "__main__":
    main()