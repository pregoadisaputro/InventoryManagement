import { api } from './client';

const LOGIN_URL = '/auth/login';

export function login(username, password) {
	return api(LOGIN_URL, {
		method: 'POST',
		body: JSON.stringify({
			username,
			password
		})
	});
}
