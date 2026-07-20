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

		const id = Number(formData.get('id') || 0);

		const name = formData.get('name')?.toString().trim() || '';
		const sku = formData.get('sku')?.toString().trim() || '';

		const description = formData.get('description')?.toString().trim() || null;
		const imgUrl = formData.get('imgUrl')?.toString().trim() || null;

		const price = Number(formData.get('price'));
		const stock = Number(formData.get('stock'));
		const minimumStock = Number(formData.get('minimumStock'));
		const categoryId = Number(formData.get('categoryId'));

		const supplierRaw = formData.get('supplierId')?.toString().trim();
		const supplierId = supplierRaw ? Number(supplierRaw) : null;

		const body = {
			name,
			sku,
			description,
			price,
			stock,
			minimumStock,
			imgUrl,
			categoryId,
			supplierId
		};

		try {
			if (id > 0) {
				await serverApi(`${PRODUCTS_URL}/${id}`, cookies, {
					method: 'PUT',
					body: JSON.stringify(body)
				});
			} else {
				await serverApi(PRODUCTS_URL, cookies, {
					method: 'POST',
					body: JSON.stringify(body)
				});
			}
		} catch (err) {
			return fail(err.status || 400, {
				errorMsg: err.body?.message || 'Failed to save product',
				errors: err.body?.errors ?? {}
			});
		}

		return {
			success: true
		};
	}
};
