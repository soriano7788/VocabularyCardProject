<template>
  <div class="col-md-4">
    <div class="card text-center border-success" style="cursor:pointer;">
      <div class="card-body">
        <h1 class="card-title display-3">{{ card.Vocabulary }}</h1>
        <h6 class="card-subtitle">
          <!-- this is subtitle -->
        </h6>
        <p class="card-text">
          <!-- card-text -->
        </p>
        <!-- 純單字和解是可拆兩部分  -->
        <!-- 正面 和 反面(翻卡片)  -->
        <!-- 或是 解釋用收合效果 -->
        <card-interpretations
          v-bind:interpretations="card.Interpretations"
        ></card-interpretations>
      </div>
    </div>
  </div>
</template>

<script>
import CardInterpretations from "../CardInterpretation/CardInterpretations.vue";

export default {
  components: {
    cardInterpretations: CardInterpretations,
  },
  props: ["card"],
  created: function() {},
};
</script>
