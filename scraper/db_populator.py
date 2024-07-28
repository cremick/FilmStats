import json

def assign_resources_to_film(resources, film_id, resource_type, scraper, client):
    for slug in resources:
        if resource_type == "themes":
            theme_title = slug[1]
            slug = slug[0]

        response = client.get_by_identifier(resource_type, slug)

        # Check if resource exists in DB
        if response.status_code == 404:
            # Add resource to DB
            if resource_type == "themes":
                resource_json = {
                    "title": theme_title,
                    "slug": slug
                }
            elif resource_type == "genres":
                resource_json = {
                    "title": slug
                }
            else:
                resource_json = scraper.fetch_person_data(slug)

            create_response = client.create(resource_type, resource_json)
            id = json.loads(create_response.text)['id'] 

        else:
            id = json.loads(response.text)['id']

        # Assign resource to film
        client.post_related(resource_type, id, "films", film_id)

def add_user_films_to_db(client, scraper, username):
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
            film_id = json.loads(create_film_response.text)['id']

            # Add cast to film
            assign_resources_to_film(cast, film_id, "actors", scraper, client)

            # Add directors to film
            assign_resources_to_film(directors, film_id, "directors", scraper, client)

            # Add genres to film
            assign_resources_to_film(genres, film_id, "genres", scraper, client)

            # Add themes to film
            assign_resources_to_film(themes, film_id, "themes", scraper, client)

        else:
            film_id = json.loads(film_response.text)['id']

        # Assign film to user
        client.watch_film(film_id)
