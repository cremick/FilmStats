import requests

class APIClient:
    def __init__(self, base_url, token):
        self.base_url = base_url
        self.headers = {
            "accept": "*/*",
            "Authorization": f"Bearer {token}"
        }

    def _request(self, method, endpoint, **kwargs):
        if "ratings" in endpoint:
            endpoint = endpoint.replace("ratings", "me/ratings")
        
        url = f"{self.base_url}/{endpoint}"

        return requests.request(method, url, headers=self.headers, **kwargs)
    
    def get_all(self, resource, user_specific = False):
        if user_specific:
            return self._request("GET", f"me/{resource}")
        return self._request("GET", resource)

    def get_by_identifier(self, resource, resource_id):
        return self._request("GET", f"{resource}/{resource_id}")

    def create(self, resource, data):
        return self._request("POST", resource, json=data)

    def update(self, resource, resource_id, data):
        return self._request("PUT", f"{resource}/{resource_id}", json=data)

    def delete(self, resource, resource_id):
        return self._request("DELETE", f"{resource}/{resource_id}")

    def get_related(self, resource, resource_id, related_resource, user_specific = False):
        if user_specific:
            return self._request("GET", f"{resource}/{resource_id}/me/{related_resource}")
        return self._request("GET", f"{resource}/{resource_id}/{related_resource}")

    def post_related(self, resource, resource_id, related_resource, related_id):
        return self._request("POST", f"{resource}/{resource_id}/{related_resource}/{related_id}")

    def delete_related(self, resource, resource_id, related_resource, related_id):
        return self._request("DELETE", f"{resource}/{resource_id}/{related_resource}/{related_id}")
    
    def watch_film(self, film_id):
        return self._request("POST", f"me/films/{film_id}/watch")
    
    def unwatch_film(self, film_id):
        return self._request("DELETE", f"me/films/{film_id}/watch")
    
    def get_rating_by_film(self, film_id):
        return self._request("GET", f"ratings/films/{film_id}")
    
    def rate_films(self, film_id, data):
        return self._request("POST", f"ratings/films/{film_id}", json=data)
