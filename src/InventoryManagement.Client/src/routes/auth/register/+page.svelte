<script>
	import { enhance } from '$app/forms';
	import Card from '$lib/components/Card.svelte';
	import Button from '$lib/components/Button.svelte';
	import { CircleCheck, CircleX } from '@lucide/svelte';

	let { form } = $props();

	let password = $state('');
	let isSubmitting = $state(false);

	const requirements = $derived(() => [
		{
			text: 'At least 8 characters',
			valid: password.length >= 8
		},
		{
			text: 'One uppercase letter',
			valid: /[A-Z]/.test(password)
		},
		{
			text: 'One lowercase letter',
			valid: /[a-z]/.test(password)
		},
		{
			text: 'One number',
			valid: /[0-9]/.test(password)
		},
		{
			text: 'One special character',
			valid: /[^a-zA-Z0-9]/.test(password)
		}
	]);

	const passwordValid = $derived(requirements.every((requirement) => requirement.valid));
</script>

<div class="flex min-h-screen items-center justify-center">
	<Card title="Freego Inventory Management" description="Register">
		{#if form?.title}
			<p class="text-red-500">
				{form.title}
			</p>
		{/if}

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

			{#if form?.errors?.Username}
				<div class="space-y-1">
					{#each form.errors.Username as error (error)}
						<p class="text-sm text-red-500">
							{error}
						</p>
					{/each}
				</div>
			{/if}

			<input
				class="w-full rounded-md border border-gray-300 px-3 py-2 focus:border-black focus:outline-none"
				name="password"
				type="password"
				bind:value={password}
				placeholder="Enter password"
				required
			/>

			<div class="mt-2 space-y-1">
				{#each requirements as requirement (requirement.text)}
					{@const Icon = requirement.valid ? CircleCheck : CircleX}

					<div class="flex items-center gap-2 text-sm">
						<span class={requirement.valid ? 'text-green-600' : 'text-gray-400'}>
							<Icon size={18} />
						</span>

						<span class={requirement.valid ? 'text-green-600' : 'text-gray-500'}>
							{requirement.text}
						</span>
					</div>
				{/each}
			</div>

			<Button
				title={isSubmitting ? 'Register...' : 'Register'}
				disabled={isSubmitting || !passwordValid}
			/>
		</form>
	</Card>
</div>
