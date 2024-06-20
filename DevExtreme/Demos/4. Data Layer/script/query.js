import { userData } from "../data/user.js";

var processArray = DevExpress.data
  .query(userData)
  //   .filter(function (dataItem) {
  //     return dataItem.name[0] == "D";
  //   })
  .sortBy("name")
  .select("name", "state")
  .toArray();

// max(getter)
// min(getter)
// slice(skip, take)
// sum(getter)
// thenBy(getter)

console.log("Query", processArray);
console.log(
  "Average",
  DevExpress.data
    .query(userData)
    // .avg("id")
    // .count()
    .enumerate()
    .done((result) => console.log(result))
);

console.log(DevExpress.data.query(userData).groupBy("state").toArray());
