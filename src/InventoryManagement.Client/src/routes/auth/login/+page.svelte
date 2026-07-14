<script>
	import { goto } from '$app/navigation';

	let username = $state('');
	let password = $state('');
	let errorMsg = $state('');

	async function handleLogin() {
		errorMsg = '';

		const response = await fetch('/auth/login', {
			method: 'POST',
			headers: { 'Content-Type': 'application/json' },
			body: JSON.stringify({ username, password })
		});

		if (!response.ok) {
			errorMsg = await response.text();
			return;
		}

		goto('/products');
	}
</script>

{#if errorMsg}
	<p>{errorMsg}</p>
{/if}

<input bind:value={username} placeholder="username" />
<input bind:value={password} placeholder="password" type="password" />

<button onclick={handleLogin}>login</button>
