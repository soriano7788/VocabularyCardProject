<template>
  <div class="col">
    <div class="card text-center border-success">
      <div class="card-body">
        <h5 class="card-title">
          {{ card.Vocabulary }}
        </h5>
        <h6 class="card-subtitle">
          this is subtitle
        </h6>
        <p class="card-text">card-text</p>
        <ul class="list-group list-group-flush">
          <li class="list-group-item">說明 1</li>
          <li class="list-group-item">說明 2</li>
          <li class="list-group-item">說明 3</li>
        </ul>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: ["card"],
  created: function() {},
};
</script>
