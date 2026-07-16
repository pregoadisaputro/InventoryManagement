<script>
	import { enhance } from '$app/forms';
	import Card from '$lib/components/Card.svelte';
	import Button from '$lib/components/Button.svelte';

	let { form } = $props();

	let isSubmitting = $state(false);
</script>

<div class="flex min-h-screen items-center justify-center">
	<Card title="Freego Inventory Management" description="Sign in to your account">
		{#if form?.title}
			<p class="rounded-md bg-red-50 p-3 text-sm text-red-600">
				{form.title}
			</p>
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
			<div>
				<input
					class="w-full rounded-md border border-gray-300 px-3 py-2 focus:border-black focus:outline-none"
					name="username"
					value={form?.username ?? ''}
					placeholder="Enter username"
					required
				/>

				{#if form?.errors?.Username}
					<div class="mt-1 space-y-1">
						{#each form.errors.Username as error (error)}
							<p class="text-sm text-red-500">{error}</p>
						{/each}
					</div>
				{/if}
			</div>

			<div>
				<input
					class="w-full rounded-md border border-gray-300 px-3 py-2 focus:border-black focus:outline-none"
					name="password"
					type="password"
					placeholder="Enter password"
					required
				/>

				{#if form?.errors?.Password}
					<div class="mt-1 space-y-1">
						{#each form.errors.Password as error (error)}
							<p class="text-sm text-red-500">{error}</p>
						{/each}
					</div>
				{/if}
			</div>

			<Button title={isSubmitting ? 'Signing in...' : 'Sign in'} disabled={isSubmitting} />
		</form>
	</Card>
</div>
