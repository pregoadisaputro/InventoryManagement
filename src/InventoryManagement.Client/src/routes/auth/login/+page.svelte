<script>
	import { enhance } from '$app/forms';
	import Card from '$lib/components/Card.svelte';
	import Button from '$lib/components/Button.svelte';

	let { form } = $props();

	let isSubmitting = $state(false);
</script>

<div class="flex min-h-screen items-center justify-center">
	<Card title="Freego Inventory Management" description="Sign in to your account">
		<form
			class="flex flex-col gap-4"
			method="POST"
			use:enhance={() => {
				isSubmitting = true;
				return async ({ update }) => {
					await update();
					isSubmitting = false;
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

			<div>
				<input
					class="w-full rounded-md border border-gray-300 px-3 py-2 focus:border-black focus:outline-none"
					name="password"
					type="password"
					placeholder="Enter password"
					required
				/>
			</div>

			<Button title={isSubmitting ? 'Signing in...' : 'Sign in'} disabled={isSubmitting} />
		</form>
	</Card>
</div>
