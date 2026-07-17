import { serverApi } from '$lib/server/api';
import { DASHBOARD_URL } from '$lib/api/dashboard/dashboardEndpoints';

export async function load({ cookies }) {
	const dashboard = await serverApi(`${DASHBOARD_URL}`, cookies);

	return { dashboard };
}
