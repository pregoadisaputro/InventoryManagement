import { json } from '@sveltejs/kit';

export async function POST({ cookies }) {
	cookies.delete('token', { path: '/' });
	return json({ sucess: true });
}
