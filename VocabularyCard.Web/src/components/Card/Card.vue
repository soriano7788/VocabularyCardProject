<template>
  <div>
    <div
      @click="flipCard"
      class="card text-center shadow bg-card text-white"
      style="cursor:pointer;"
    >
      <div class="card-body">
        <i @click.stop="editCard" class="bi bi-pencil edit-icon"></i>
        <transition name="flip" mode="out-in">
          <card-front v-if="face == 'front'" :card="card"></card-front>
          <card-back v-else>
            <card-interpretations
              v-bind:interpretations="card.Interpretations"
            ></card-interpretations>
          </card-back>
        </transition>
      </div>
    </div>
  </div>
</template>

<script>
import CardInterpretations from "../CardInterpretation/CardInterpretations.vue";
import CardFront from "./CardFront.vue";
import CardBack from "./CardBack.vue";

export default {
  components: {
    cardInterpretations: CardInterpretations,
    cardFront: CardFront,
    cardBack: CardBack,
  },
  props: ["card"],
  data() {
    return {
      face: "front",
    };
  },
  methods: {
    editCard() {
      this.$emit("showEditCard", this.card);
    },
    flipCard() {
      this.face = this.face == "front" ? "back" : "front";
    },
  },
  created: function() {},
};
</script>

<style scoped>
.bg-card {
  background-color: #38444f;
}
.edit-icon {
  float: right;
}
.flip-enter {
  /* transform: rotate(0deg); */
}
.flip-enter-active {
  animation: flip-in 0.3s ease-out forwards;
}
.flip-leave {
  transform: rotateY(0deg);
}
.flip-leave-active {
  animation: flip-out 0.3s ease-out forwards;
}
@keyframes flip-in {
  from {
    transform: rotateY(90deg);
  }
  to {
    transform: rotateY(0deg);
  }
}
@keyframes flip-out {
  from {
    transform: rotateY(0deg);
  }
  to {
    transform: rotateY(90deg);
  }
}
</style>
