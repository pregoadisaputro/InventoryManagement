import { fail, redirect } from '@sveltejs/kit';
import { serverApi } from '$lib/server/api.js';
import { PRODUCTS_URL } from '$lib/api/products/productsEndpoint.js';

export const actions = {
	default: async ({ request, cookies }) => {
		const formData = await request.formData();

		const name = formData.get('name')?.toString() || '';
		const sku = formData.get('sku')?.toString() || '';

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

		throw redirect(303, '/products');
	}
};
