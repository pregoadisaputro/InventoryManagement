import { AUTH_URL } from '$lib/api/auth/authEndpoints.js';
import { dev } from '$app/environment';
import { serverApi } from '$lib/server/api.js';
import { fail, redirect } from '@sveltejs/kit';

export const actions = {
	default: async ({ request, cookies }) => {
		const formData = await request.formData();

		const username = formData.get('username')?.toString().trim() || '';
		const password = formData.get('password')?.toString().trim() || '';

		try {
			const data = await serverApi(`${AUTH_URL}/login`, cookies, {
				method: 'POST',
				body: JSON.stringify({
					username,
					password
				})
			});

			cookies.set('token', data.token, {
				path: '/',
				httpOnly: true,
				secure: !dev,
				sameSite: 'strict',
				expires: new Date(data.expiresAt)
			});
		} catch (err) {
			return fail(err.status || 400, {
				errMsg: err.body?.message || 'Failed to login'
			});
		}

		throw redirect(303, '/products');
	}
};
