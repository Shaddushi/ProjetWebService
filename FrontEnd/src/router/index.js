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
    }
  ],
})

export default router
