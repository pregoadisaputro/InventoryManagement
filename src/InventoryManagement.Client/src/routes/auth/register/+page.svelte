<script>
	import { enhance } from '$app/forms';
	import Card from '$lib/components/Card.svelte';
	import Button from '$lib/components/Button.svelte';

	let { form } = $props();
	let isSubmitting = $state(false);
</script>

<div class="flex min-h-screen items-center justify-center">
	<Card title="Freego Inventory Management" description="Register">
		{#if form?.title}
			<p class="text-red-500">{form.title}</p>
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

			{#if form?.errors?.Username}
				<div class="space-y-1">
					{#each form.errors.Username as error (error)}
						<p class="text-sm text-red-500">{error}</p>
					{/each}
				</div>
			{/if}

			<input
				class="w-full rounded-md border border-gray-300 px-3 py-2 focus:border-black focus:outline-none"
				name="password"
				type="password"
				placeholder="Enter password"
				required
			/>

			{#if form?.errors?.Password}
				<div class="space-y-1">
					{#each form.errors.Password as error (error)}
						<p class="text-sm text-red-500">{error}</p>
					{/each}
				</div>
			{/if}

			<Button title={isSubmitting ? 'Register...' : 'Register'} disabled={isSubmitting} />
		</form>
	</Card>
</div>
