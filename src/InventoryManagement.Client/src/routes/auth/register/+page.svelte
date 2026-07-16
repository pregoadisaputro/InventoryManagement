<script>
	import { enhance } from '$app/forms';
	import Card from '$lib/components/Card.svelte';
	import Button from '$lib/components/Button.svelte';

	let { form } = $props();
	let isSubmitting = $state(false);
</script>

<div class="flex min-h-screen items-center justify-center">
	<Card title="Freego Inventory Management" description="Register">
		{#if form?.errorMsg}
			<p style="color: red;">{form.errorMsg}</p>
		{/if}

		<form
			class="flex flex-col gap-4"
			method="POST"
			use:enhance={() => {
				isSubmitting = true;
				return async ({ update }) => {
					isSubmitting = false;
					await update();
				};
			}}
		>
			<input
				class="w-full rounded-md border border-gray-300 px-3 py-2 focus:border-black focus:outline-none"
				name="username"
				value={form?.username ?? ''}
				placeholder="Enter username"
				required
			/>
			<input
				class="w-full rounded-md border border-gray-300 px-3 py-2 focus:border-black focus:outline-none"
				name="password"
				type="password"
				placeholder="Enter password"
				required
			/>

			<Button title={isSubmitting ? 'Register...' : 'Register'} disabled={isSubmitting} />
		</form>
	</Card>
</div>
