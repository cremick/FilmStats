import requests
from config import BASE_URL, TOKEN

class APIClient:
    def __init__(self, base_url, token):
        self.base_url = base_url
        self.headers = {
            "accept": "*/*",
            "Authorization": f"Bearer {token}"
        }

    def _request(self, method, endpoint, user_specific, **kwargs):
        url = f"{self.base_url}/{endpoint}"
        if user_specific:
            url = f"{self.base_url}/me/{endpoint}"
        
        return requests.request(method, url, headers=self.headers, **kwargs)
    
    def get_all(self, resource, user_specific):
        return self._request("GET", resource, user_specific)

    def get_by_identifier(self, resource, resource_id, user_specific):
        return self._request("GET", f"{resource}/{resource_id}", user_specific)

    def create(self, resource, data, user_specific):
        return self._request("POST", resource, user_specific, json=data)

    def update(self, resource, resource_id, data, user_specific):
        return self._request("PUT", f"{resource}/{resource_id}", user_specific, json=data)

    def delete(self, resource, resource_id, user_specific):
        return self._request("DELETE", f"{resource}/{resource_id}", user_specific)

    def get_related(self, resource, resource_id, related_resource, user_specific):
        return self._request("GET", f"{resource}/{resource_id}/{related_resource}", user_specific)

    def post_related(self, resource, resource_id, related_resource, related_id, user_specific):
        return self._request("POST", f"{resource}/{resource_id}/{related_resource}/{related_id}", user_specific)

    def delete_related(self, resource, resource_id, related_resource, related_id, user_specific):
        return self._request("DELETE", f"{resource}/{resource_id}/{related_resource}/{related_id}", user_specific)


def main():
    api_client = APIClient(BASE_URL, TOKEN)
    resources = ["films", "actors", "directors", "people", "genres", "themes", "ratings"]

    user_specific = False
    
    for resource in resources:
        api_client.get_all(resource, user_specific)
        api_client.get_by_identifier(resource, "id", user_specific)
        api_client.create(resource, {}, user_specific)
        api_client.update(resource, "id", {}, user_specific)
        api_client.delete(resource, "id", user_specific)
        api_client.get_related(resource, "id", "related", user_specific)
        api_client.post_related(resource, "id", "related", "related_id", user_specific)
        api_client.delete_related(resource, "id", "related", "related_id", user_specific)

    user_specific = True

    for resource in resources:
        api_client.get_all(resource, user_specific)
        api_client.get_by_identifier(resource, "id", user_specific)
        api_client.create(resource, {}, user_specific)
        api_client.update(resource, "id", {}, user_specific)
        api_client.delete(resource, "id", user_specific)
        api_client.get_related(resource, "id", "related", user_specific)
        api_client.post_related(resource, "id", "related", "related_id", user_specific)
        api_client.delete_related(resource, "id", "related", "related_id", user_specific)
        
if __name__ == "__main__":
    main()
