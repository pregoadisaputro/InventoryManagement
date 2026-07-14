import { error } from '@sveltejs/kit';
import { API_URL } from '$lib/api/apiUrl';

export async function serverApi(path, cookies, options = {}) {
	const token = cookies.get('token');

	const response = await fetch(`${API_URL}${path}`, {
		...options,
		headers: {
			'Content-Type': 'application/json',
			...(token && { Authorization: `Bearer ${token}` }),
			...options.headers
		}
	});

	if (!response.ok) {
		throw error(response.status, await response.text());
	}

	return response.json();
}
