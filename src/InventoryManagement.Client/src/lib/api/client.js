const API_URL = 'http://localhost:5005';

export async function api(path, options = {}) {
	const token = localStorage.getItem('token');

	const response = await fetch(`${API_URL}${path}`, {
		...options,
		headers: {
			'Content-Type': 'application/json',
			...(token && {
				Authorization: `Bearer ${token}`
			}),
			...options.headers
		}
	});

	if (!response.ok) {
		throw new Error(await response.text());
	}

	return response.json();
}
