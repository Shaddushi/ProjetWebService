import { createRouter, createWebHistory } from 'vue-router'
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    { path: '/', 
      redirect: '/profile',
    },
    {
      path: '/tracks',
      name: 'Tracks', 
      component: () => import('@/components/views/TrackView.vue')
    },
    {
      path: '/profile',
      name: 'Profile',
      component: () => import('@/components/views/ProfileView.vue')
    },
    {
      path: '/artists',
      name: 'Artist',
      component: () => import('@/components/views/ArtistView.vue')
    },
    {
      path: '/albums',
      name: 'Album',
      component: () => import('@/components/views/AlbumView.vue')
    },
    {
      path: '/track/:id',
      name: 'TrackDetails',
      component: () => import('@/components/singularComponent/Track.vue'),
    },
    {
      path: '/album/:id',
      name: 'AlbumDetails',
      component: () => import('@/components/singularComponent/Album.vue'),
    },
    {
      path: '/artist/:id',
      name: 'ArtistDetails',
      component: () => import('@/components/singularComponent/Artist.vue'),
    }
  ],
})

export default router
