import { json, error } from '@sveltejs/kit';
import { API_URL } from '$lib/api/apiUrl.js';

export async function POST({ request, cookies }) {
	const { username, password } = await request.json();

	const response = await fetch(`${API_URL}/auth/login`, {
		method: 'POST',
		headers: { 'Content-Type': 'application/json' },
		body: JSON.stringify({ username, password })
	});

	if (!response.ok) {
		throw error(response.status, await response.text());
	}

	const data = await response.json();
	const expiresAt = new Date(data.expiresAt);

	cookies.set('token', data.token, {
		path: '/',
		httpOnly: true,
		secure: process.env.NODE_ENV === 'production',
		sameSite: 'strict',
		expires: expiresAt
	});

	return json({ username: data.username });
}
