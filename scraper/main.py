from api_client import APIClient
from web_scraper import LetterboxdScraper
from db_populator import DataIntegrator
import json
import ast
from config import BASE_URL, TOKEN

def main():
    api_client = APIClient(BASE_URL, TOKEN)
    scraper = LetterboxdScraper()
    integrator = DataIntegrator(api_client, scraper)
    username = "camrynremick"

    # integrator.add_user_films_to_db(username)

    '''
    user_films = scraper.fetch_user_films(username)
    for film in user_films:
        integrator.check_resources(film, "directors")
        integrator.check_resources(film, "genres")
        integrator.check_resources(film, "themes")
    '''

    actors = ast.literal_eval(api_client.get_all("actors").text)
    birthdayless = []
    for actor in actors:
        if actor["birthDate"] == "0001-01-01T00:00:00":
            birthdayless.append(actor)

    # print(len(birthdayless))
    for b in birthdayless:
        print(b['slug'])


if __name__ == "__main__":
    main()