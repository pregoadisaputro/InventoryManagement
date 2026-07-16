<script>
	import { enhance } from '$app/forms';

	let { form } = $props();
	let isSubmitting = $state(false);
</script>

<h1>Login</h1>

{#if form?.errorMsg}
	<p style="color: red;">{form.errorMsg}</p>
{/if}

<form
	method="POST"
	use:enhance={() => {
		isSubmitting = true;
		return async ({ update }) => {
			isSubmitting = false;
			await update();
		};
	}}
	<input
	name="username"
	placeholder="username"
	required
>
	<input name="password" type="password" placeholder="password" required />
	>

	<button type="submit" disabled={isSubmitting}>
		{isSubmitting ? 'login...' : 'Login'}
	</button>
</form>

<style>
	form {
		display: flex;
		gap: 0.75rem;
		max-width: 400px;
	}
</style>
