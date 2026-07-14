export async function handle({ event, resolve }) {
	const token = event.cookies.get('token');
	event.locals.isAuthenticated = !!token;

	return resolve(event);
}
