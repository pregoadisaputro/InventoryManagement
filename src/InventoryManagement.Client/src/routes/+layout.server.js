import { USER_URL } from '$lib/api/users/usersEndpoints.js';
import { serverApi } from '$lib/server/api';

export async function load({ cookies }) {
	try {
		const user = await serverApi(`${USER_URL}/me`, cookies);

		return {
			user
		};
	} catch {
		return {
			user: null
		};
	}
}
