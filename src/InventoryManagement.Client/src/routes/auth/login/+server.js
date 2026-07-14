import { json } from '@sveltejs/kit';
import { dev } from '$app/environment';
import { serverApi } from '$lib/server/api.js';

export async function POST({ request, cookies }) {
	const { username, password } = await request.json();

	const data = await serverApi('/auth/login', cookies, {
		method: 'POST',
		body: JSON.stringify({ username, password })
	});

	const expiresAt = new Date(data.expiresAt);

	cookies.set('token', data.token, {
		path: '/',
		httpOnly: true,
		secure: !dev,
		sameSite: 'strict',
		expires: expiresAt
	});

	return json({ username: data.username });
}
