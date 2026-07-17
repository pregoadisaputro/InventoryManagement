<script>
	import { Tags } from '@lucide/svelte';
	import HouseIcon from '@lucide/svelte/icons/house';
	import { Package } from '@lucide/svelte';
	import { ScrollText } from '@lucide/svelte';
	import SettingsIcon from '@lucide/svelte/icons/settings';
	import * as Sidebar from '$lib/components/ui/sidebar/index.js';

	const items = [
		{
			title: 'Dashboard',
			url: '/dashboard',
			icon: HouseIcon
		},
		{
			title: 'Inventory',
			url: '/dashboard/products',
			icon: Package
		},
		{
			title: 'Categories',
			url: '#',
			icon: Tags
		},
		{
			title: 'Transactions',
			url: '#',
			icon: ScrollText
		},
		{
			title: 'Settings',
			url: '#',
			icon: SettingsIcon
		}
	];
</script>

<Sidebar.Root>
	<Sidebar.Content>
		<Sidebar.Group>
			<Sidebar.GroupLabel>Freego Inventory Management</Sidebar.GroupLabel>
			<Sidebar.GroupContent>
				<Sidebar.Menu>
					{#each items as item (item.title)}
						<Sidebar.MenuItem>
							<Sidebar.MenuButton>
								{#snippet child({ props })}
									<a href={item.url} {...props}>
										<item.icon />
										<span>{item.title}</span>
									</a>
								{/snippet}
							</Sidebar.MenuButton>
						</Sidebar.MenuItem>
					{/each}
				</Sidebar.Menu>
			</Sidebar.GroupContent>
		</Sidebar.Group>
	</Sidebar.Content>
</Sidebar.Root>
