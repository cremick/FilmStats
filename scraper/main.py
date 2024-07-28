from api_client import APIClient
from web_scraper import LetterboxdScraper
from config import BASE_URL, TOKEN

def main():
    api_client = APIClient(BASE_URL, TOKEN)
    scraper = LetterboxdScraper(api_client)

    # print(scraper.fetch_person_data("julia-roberts"))
    cast = scraper.fetch_film_data("juno")['cast']

    for actor_slug in cast:
        print(scraper.fetch_person_data(actor_slug))
    
    # print(scraper.fetch_user_films("sallydarr"))
    # print(scraper.fetch_theme_title("mini-theme/boxing-fighting-champion-fighter-underdog"))
    

if __name__ == "__main__":
    main()