import { API_URL } from '$lib/api/apiUrl';

export async function serverApi(path, cookies, options = {}) {
	const token = cookies?.get('token');

	const response = await fetch(`${API_URL}${path}`, {
		...options,
		headers: {
			'Content-Type': 'application/json',
			...(token && { Authorization: `Bearer ${token}` }),
			...options.headers
		}
	});

	if (!response.ok) {
		let body;

		try {
			body = await response.json();
		} catch {
			body = {
				title: await response.text()
			};
		}

		throw {
			status: response.status,
			body
		};
	}

	return response.json();
}
