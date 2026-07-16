import { json } from '@sveltejs/kit';

export const actions = {
	default: async ({ cookies }) => {
		cookies.delete('token', { path: '/' });
		return json({ sucess: true });
	}
};
