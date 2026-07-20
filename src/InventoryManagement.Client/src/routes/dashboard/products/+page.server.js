import { serverApi } from '$lib/server/api';
import { fail } from '@sveltejs/kit';
import { PRODUCTS_URL } from '$lib/api/products/productsEndpoint';
import { CATEGORIES_URL } from '$lib/api/categories/categoriesEndpoints.js';
import { SUPPLIERS_URL } from '$lib/api/suppliers/suppliersEndpoints.js';

export async function load({ url, cookies }) {
	const pageNumber = Math.max(1, Number(url.searchParams.get('pageNumber')) || 1);
	const pageSize = Math.max(1, Number(url.searchParams.get('pageSize')) || 5);

	const [page, categories, suppliers] = await Promise.all([
		serverApi(`${PRODUCTS_URL}?pageNumber=${pageNumber}&pageSize=${pageSize}`, cookies),
		serverApi(CATEGORIES_URL, cookies),
		serverApi(SUPPLIERS_URL, cookies)
	]);

	return {
		page,
		categories,
		suppliers
	};
}

export const actions = {
	default: async ({ request, cookies }) => {
		const formData = await request.formData();

		const name = formData.get('name')?.toString().trim() || '';
		const sku = formData.get('sku')?.toString().trim() || '';

		const description = formData.get('description')?.toString().trim() || null;
		const imgUrl = formData.get('imgUrl')?.toString().trim() || null;

		const price = parseFloat(formData.get('price')?.toString() || '0');
		const stock = parseInt(formData.get('stock')?.toString() || '0', 10);
		const minimumStock = parseInt(formData.get('minimumStock')?.toString() || '0', 10);
		const categoryId = parseInt(formData.get('categoryId')?.toString() || '0', 10);

		const supplierIdRaw = formData.get('supplierId')?.toString().trim();
		const supplierId = supplierIdRaw ? parseInt(supplierIdRaw, 10) : null;

		try {
			await serverApi(PRODUCTS_URL, cookies, {
				method: 'POST',
				body: JSON.stringify({
					name,
					sku,
					description,
					price,
					stock,
					minimumStock,
					imgUrl,
					categoryId,
					supplierId
				})
			});
		} catch (err) {
			return fail(err.status || 400, {
				errorMsg: err.body?.message || 'Failed to create product'
			});
		}

		return {
			success: true
		};
	}
};
