<script>
	import { login } from '$lib/api/auth';

	let username = $state('');
	let password = $state('');

	async function handleLogin() {
		try {
			const response = await login(username, password);

			console.log(response);
		} catch (error) {
			console.log(error);
		}
	}
</script>

<input bind:value={username} placeholder="username" />

<input type="password" bind:value={password} placeholder="password" />

<button onclick={handleLogin}>login</button>
