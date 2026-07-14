import { serverApi } from '$lib/server/api';
import { PRODUCTS_URL } from '$lib/api/products/productsEndpoint';

export async function load({ url, cookies }) {
	const pageNumber = Math.max(1, Number(url.searchParams.get('pageNumber')) || 1);
	const pageSize = Math.max(1, Number(url.searchParams.get('pageSize')) || 5);

	const page = await serverApi(
		`${PRODUCTS_URL}?pageNumber=${pageNumber}&pageSize=${pageSize}`,
		cookies
	);
	return { page };
}
