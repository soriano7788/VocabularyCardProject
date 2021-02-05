<template>
  <div class="row">
    <h1 class="card-title display-3">
      {{ card.Vocabulary }}
    </h1>
  </div>
</template>

<script>
export default {
  props: ["card"],
};
</script>
