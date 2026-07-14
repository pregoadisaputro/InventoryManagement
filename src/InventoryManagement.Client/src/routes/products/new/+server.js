import { PRODUCTS_URL } from '$lib/api/products/productsEndpoint.js';
import { serverApi } from '$lib/server/api';
import { json } from '@sveltejs/kit';

export async function POST({ request, cookies }) {
	const product = await request.json();

	const data = await serverApi(`${PRODUCTS_URL}`, cookies, {
		method: 'POST',
		body: JSON.stringify(product)
	});

	return json(data);
}
