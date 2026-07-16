import { AUTH_URL } from '$lib/api/auth/authEndpoints';
import { serverApi } from '$lib/server/api';
import { fail, redirect } from '@sveltejs/kit';

export const actions = {
	default: async ({ request }) => {
		const formData = await request.formData();

		const username = formData.get('username')?.toString().trim() || '';
		const password = formData.get('password')?.toString().trim() || '';

		try {
			await serverApi(`${AUTH_URL}/register`, {
				method: 'POST',
				body: JSON.stringify({ username, password })
			});
		} catch (err) {
			return fail(err.status || 400, {
				username,
				title: err.body?.title,
				errors: err.body?.errors ?? {}
			});
		}

		throw redirect(303, '/auth/login');
	}
};
