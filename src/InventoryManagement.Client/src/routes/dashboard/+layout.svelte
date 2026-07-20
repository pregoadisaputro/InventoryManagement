<script>
	import AppSidebar from '$lib/components/AppSidebar.svelte';
	import * as Sidebar from '$lib/components/ui/sidebar';

	let { children } = $props();
</script>

<Sidebar.Provider>
	<AppSidebar />
	<main class="min-w-0 flex-1 p-4">
		<Sidebar.Trigger />
		{@render children?.()}
	</main>
</Sidebar.Provider>
