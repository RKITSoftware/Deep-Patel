export const hieararchicalData = [
  {
    id: "1",
    name: "Electronics",
    expanded: true,
    items: [
      {
        id: "1_1",
        name: "Laptop",
        expanded: false,
        items: [
          {
            id: "1_1_1",
            name: "Lenovo",
          },
          {
            id: "1_1_2",
            name: "Asus",
          },
          {
            id: "1_1_3",
            name: "Dell",
          },
        ],
      },
      {
        id: "1_2",
        name: "Mobile Phone",
        expanded: true,
        items: [
          {
            id: "1_2_1",
            name: "Apple",
          },
          {
            id: "1_2_2",
            name: "Samsung",
            disabledItem: true,
          },
        ],
      },
    ],
  },
  {
    id: "2",
    name: "Grooceries",
    expanded: false,
    items: [
      {
        id: "2_1",
        name: "Sugar",
      },
      {
        id: "2_2",
        name: "Salt",
      },
    ],
  },
];
