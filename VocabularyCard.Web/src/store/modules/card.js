import axios from "axios";
import * as statusCode from "../consts/statusCode";
import router from "../../routes.js";

const state = {
    cards: []
};

const getters = {
    cards: state => {
        return state.cards;
    }
};

const mutations = {
    setCards: (state, payload) => {
        state.cards = payload;
    },
    appendNewCard: (state, card) => {
        state.cards.push(card);
    },
    updateCard: (state, card) => {
        var target = state.cards.find(function (item) {
            return item.CardId == card.CardId;
        });

        console.log("target: ", target);

        if (target == undefined || target == null) {
            state.cards.push(card);
        } else {
            Object.assign(target, card);
            // target = card;
        }
    }
};

const actions = {
    fetchCardSetAllCards: ({ commit }, cardSetId) => {
        axios.get("cardset/GetCards/" + cardSetId)
            .then(res => {
                const result = res.data;
                if (result.statusCode == statusCode.SUCCESS) {
                    const cards = result.data;
                    commit("setCards", cards);
                }
            })
            .catch(error => {
                console.log(error);
            });
    },
    clearCards: ({ commit }) => {
        commit("setCards", []);
    },
    createCard: ({ commit }, payload) => {
        axios.post("card/create/" + payload.cardSetId, payload.card)
            .then(res => {
                const result = res.data;
                if (result.statusCode == statusCode.SUCCESS) {
                    const newCard = result.data;
                    commit("appendNewCard", newCard);
                }
            })
            .catch(error => {
                console.log(error);
            });
    },
    updateCard: ({ commit }, card) => {
        return axios.put("card/update", card)
            .then(res => {
                const result = res.data;
                if (result.statusCode == statusCode.SUCCESS) {
                    // 還需要更新目前 state 裡面的 Card 資料
                    commit("updateCard", card);
                    Promise.resolve();
                }

            }).catch(error => {
                Promise.reject();
            });
    }
}

export default {
    state: state,
    getters: getters,
    mutations: mutations,
    actions: actions
};