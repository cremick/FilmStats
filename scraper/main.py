from api_client import APIClient
from web_scraper import LetterboxdScraper, TMDBScraper
from db_populator import DataIntegrator
import json
import ast
from config import BASE_URL, TOKEN, TMDB_TOKEN
import time

def main():
    api_client = APIClient(BASE_URL, TOKEN)
    scraper = LetterboxdScraper(TMDB_TOKEN)
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
    # print(scraper.fetch_person_data('jenna-fischer'))
    
    '''
    actors = ast.literal_eval(api_client.get_all("people").text)
    birthdayless = []
    for actor in actors:
        if actor["birthDate"] == "0001-01-01T00:00:00":
            birthdayless.append(actor)

    print(len(birthdayless))
    
    for index, b in enumerate(birthdayless, start=1):
        slug = b['slug']
        print(f'{index}. {slug}')
        # url = scraper.get_tmdb_link(slug)
        # new_birthday = tmdb_scraper.get_birthday(url)
        new_data = scraper.fetch_person_data(slug)
        
        if new_data['birthDate'] != '0001-01-01':
            print(new_data)
            api_client.update("people", b['id'], new_data)
    '''

    # Compare new and old person data
    all_people = ast.literal_eval(api_client.get_all("people").text)
    print(len(all_people))
    for index, person in enumerate(all_people, start=1):
        time.sleep(0.1)
        person_slug = person['slug']
        print(f"{index} -----------{person_slug}-----------")
        new_person_data = scraper.fetch_person_data(person_slug)

        og_birth_date = person['birthDate'][:10]
        og_death_date = person['deathDate'][:10]

        different = False

        # Compare gender, birthday, death date
        if person['gender'] != new_person_data['gender']:
            if new_person_data['gender'] == '':
                print("losing data")
            print(f"different gender - old: {person['gender']} new: {new_person_data['gender']}")
            different = True

        if og_birth_date != new_person_data['birthDate']:
            if new_person_data['birthDate'] == '0001-01-01':
                print("losing data")
            print(f"different birthday - old: {og_birth_date} new: {new_person_data['birthDate']}")
            different = True

        if og_death_date != new_person_data['deathDate']:
            if new_person_data['deathDate'] == '0001-01-01':
                print("losing data")
            print(f"different death day - old: {og_death_date} new: {new_person_data['deathDate']}")
            different = True

        if different:
            api_client.update("people", person['id'], new_person_data)
    
if __name__ == "__main__":
    main()