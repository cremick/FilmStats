from api_client import APIClient
from web_scraper import LetterboxdScraper
from config import BASE_URL, TOKEN

def assign_resource_to_film(resources, film_id, resource_type, scraper, client):
    for slug in resources:
        if resource_type == "themes":
            theme_title = slug[1]
            slug = slug[0]

        response = client.get_by_identifier(resource_type, slug)

        # Check if person exists in DB
        if response.status_code == 404:
            # Add person to DB
            json = scraper.fetch_person_data(slug)
            create_response = client.create("people", json)
            person_id = create_response.text['id'] 

        else:
            person_id = response.text['id']

        # Assign person to film
        client.post_related(person_type, person_id, "films", film_id)

def assign_people_to_film(people, film_id, person_type, scraper, client):
    for slug in people:
        response = client.get_by_identifier("people", slug)

        # Check if person exists in DB
        if response.status_code == 404:
            # Add person to DB
            json = scraper.fetch_person_data(slug)
            create_response = client.create("people", json)
            person_id = create_response.text['id'] 

        else:
            person_id = response.text['id']

        # Assign person to film
        client.post_related(person_type, person_id, "films", film_id)

def assign_categories_to_film(categories, film_id, category_type, scraper, client):
    for slug in categories:
        if category_type == "genres":
            response = client.get_by_identifier(category_type, slug)
        else:
            response = client.get_by_identifier(category_type, slug[0])

        # Check if category exists in DB
        if response.status_code == 404:
            # Add cateogry to DB
            if category_type == "genres":
                json = {
                    "title": slug
                }

            else:
                json = {
                    "title": slug[1],
                    "slug": slug[0]
                }
            create_response = client.create(category_type, json)
            category_id = response.text['id']
        
        else:
            category_id = response.text['id']

        # Assign cateogry to film
        client.post_related(category_type, category_id, "films", film_id)

def main():
    client = APIClient(BASE_URL, TOKEN)
    scraper = LetterboxdScraper()
    username = 'camrynremick'

    user_films = scraper.fetch_user_films(username)
    
    for film_slug in user_films:
        film_response = client.get_by_identifier("films", film_slug)
        
        # Check if film exists in DB
        if film_response.status_code == 404:
            # Add film to DB
            film_data = scraper.fetch_film_data(film_slug)
            
            film_json = film_data['film_data']
            cast = film_data['cast']
            directors = film_data['directors']
            genres = film_data['genres']
            themes = film_data['themes']

            create_film_response = client.create("films", film_json)
            film_id = create_film_response.text['id']

            # Add cast to film
            assign_people_to_film(cast, film_id, "actors", scraper, client)

            # Add directors to film
            assign_people_to_film(directors, film_id, "directors", scraper, client)

            # Add genres to film

            # Add themes to film

        else:
            film_id = film_response.text['id']

        # Assign film to user
        client.watch_film(film_id)

        
        


if __name__ == "__main__":
    main()
