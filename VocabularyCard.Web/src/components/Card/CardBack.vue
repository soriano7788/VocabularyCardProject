<template>
  <div class="row bg-card p-4 rounded">
    <slot></slot>
  </div>
</template>

<script>
export default {
  props: ["card"],
};
</script>
