import { serverApi } from '$lib/server/api';
import { PRODUCTS_URL } from '$lib/api/products/productsEndpoint.js';

export async function load({ params, cookies }) {
	const product = await serverApi(`${PRODUCTS_URL}/${params.id}`, cookies);
	return { product };
}
