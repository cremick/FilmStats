import json
import ast

class DataIntegrator:
    def __init__(self, client, scraper):
        self.client = client
        self.scraper = scraper

    def assign_resources_to_film(self, resources, film_id, resource_type):
        for slug in resources:
            if resource_type == "themes":
                theme_title = slug[1]
                slug = slug[0]

            if resource_type == "directors" or resource_type == "actors":
                response = self.client.get_by_identifier("people", slug)
            else:
                response = self.client.get_by_identifier(resource_type, slug)

            # Check if resource exists in DB
            if response.status_code == 404:
                print("adding resource to db")
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
                    resource_json = self.scraper.fetch_person_data(slug)
                    print(resource_json)

                if resource_type == "directors" or resource_type == "actors":
                    create_response = self.client.create("people", resource_json)
                else: 
                    create_response = self.client.create(resource_type, resource_json)
                
                print(create_response.text)
                id = json.loads(create_response.text)['id'] 

            else:
                print("resource is already in db")
                id = json.loads(response.text)['id']

            # Assign resource to film
            self.client.post_related(resource_type, id, "films", film_id)

    def add_user_films_to_db(self, username):
        user_films = self.scraper.fetch_user_films(username)
        
        for film_slug in user_films:
            film_response = self.client.get_by_identifier("films", film_slug)
            print(f"-------CURRENT FILM: {film_slug}-------")
            
            # Check if film exists in DB
            if film_response.status_code == 404:
                print(f"ADDING TO DB")
                # Add film to DB
                film_data = self.scraper.fetch_film_data(film_slug)
                
                film_json = film_data['film_data']
                cast = film_data['actors']
                directors = film_data['directors']
                genres = film_data['genres']
                themes = film_data['themes']

                create_film_response = self.client.create("films", film_json)
                film_id = json.loads(create_film_response.text)['id']

                # Add cast to film
                print(f"ADDING CAST")
                self.assign_resources_to_film(cast, film_id, "actors")

                # Add directors to film
                print(f"ADDING DIRECTORS")
                self.assign_resources_to_film(directors, film_id, "directors")

                # Add genres to film
                print(f"ADDING GENRES")
                self.assign_resources_to_film(genres, film_id, "genres")

                # Add themes to film
                print(f"ADDING THEMES")
                self.assign_resources_to_film(themes, film_id, "themes")

            else:
                print(f"ALREADY IN DB")
                film_id = json.loads(film_response.text)['id']

            # Assign film to user
            self.client.watch_film(film_id)

    def check_resources(self, film_slug, resource_type):
        # Get film data
        film_data = self.scraper.fetch_film_data(film_slug)
        all_resources = film_data[resource_type]
        film_response = self.client.get_by_identifier("films", film_slug)
        film_id = json.loads(film_response.text)['id']

        # Get resources currently assign to film in DB 
        current_resources = ast.literal_eval(self.client.get_related("films", film_id, resource_type).text)

        # If DB does not have all resources
        if len(all_resources) != len(current_resources):
            self.assign_resources_to_film(all_resources, film_id, resource_type)
