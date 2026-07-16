import { serverApi } from '$lib/server/api';

export async function load({ cookies }) {
	try {
		const user = await serverApi('/users', cookies);

		return {
			user
		};
	} catch {
		return {
			user: null
		};
	}
}
