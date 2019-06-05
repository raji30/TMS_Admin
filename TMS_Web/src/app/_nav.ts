// interface NavAttributes {
//   [propName: string]: any;
// }
// interface NavWrapper {
//   attributes: NavAttributes;
//   element: string;
// }
// interface NavBadge {
//   text: string;
//   variant: string;
// }
// interface NavLabel {
//   class?: string;
//   variant: string;
// }

// export interface NavData {
//   name?: string;
//   url?: string;
//   icon?: string;
//   badge?: NavBadge;
//   title?: boolean;
//   children?: NavData[];
//   variant?: string;
//   attributes?: NavAttributes;
//   divider?: boolean;
//   class?: string;
//   label?: NavLabel;
//   wrapper?: NavWrapper;
// }

// export const navItems = [
//   {
//     name: 'Dashboard',
//     url: '/dashboard',
//     icon: 'icon-speedometer',   
//   },
//   {
//     name: 'Orders',
//     icon: 'icon-star',
//     url: '/orderList'
//   },
//   {
//     name: 'Do-Intake',
//     icon: 'icon-pencil',
//     url: '/doIntake',
//     badge: {
//       variant: 'info',
//       text: 'New'
//     }
//   },
//   {
//     name: 'Dispatch',
//     icon: 'icon-star',
//     url: '/doIntake'
//   },
//   {
//         name: 'Base',
//         url: '/base',
//         icon: 'icon-puzzle',
//         children: [
//           {
//             name: 'Cards',
//             url: '/base/cards',
//             icon: 'icon-puzzle'
//           },
//           {
//             name: 'Carousels',
//             url: '/base/carousels',
//             icon: 'icon-puzzle'
//           },
//           {
//             name: 'Collapses',
//             url: '/base/collapses',
//             icon: 'icon-puzzle'
//           },
//           {
//             name: 'Forms',
//             url: '/base/forms',
//             icon: 'icon-puzzle'
//           },
//           {
//             name: 'Pagination',
//             url: '/base/paginations',
//             icon: 'icon-puzzle'
//           },
//           {
//             name: 'Popovers',
//             url: '/base/popovers',
//             icon: 'icon-puzzle'
//           },
//           {
//             name: 'Progress',
//             url: '/base/progress',
//             icon: 'icon-puzzle'
//           },
//           {
//             name: 'Switches',
//             url: '/base/switches',
//             icon: 'icon-puzzle'
//           },
//           {
//             name: 'Tables',
//             url: '/base/tables',
//             icon: 'icon-puzzle'
//           },
//           {
//             name: 'Tabs',
//             url: '/base/tabs',
//             icon: 'icon-puzzle'
//           },
//           {
//             name: 'Tooltips',
//             url: '/base/tooltips',
//             icon: 'icon-puzzle'
//           }
//         ]
//       },
// ];

interface NavAttributes {
  [propName: string]: any;
}
interface NavWrapper {
  attributes: NavAttributes;
  element: string;
}
interface NavBadge {
  text: string;
  variant: string;
}
interface NavLabel {
  class?: string;
  variant: string;
}

export interface NavData {
  name?: string;
  url?: string;
  icon?: string;
  badge?: NavBadge;
  title?: boolean;
  children?: NavData[];
  variant?: string;
  attributes?: NavAttributes;
  divider?: boolean;
  class?: string;
  label?: NavLabel;
  wrapper?: NavWrapper;
}

export const navItems: NavData[] = [
  {
    name: 'Dashboard',
    url: '/dashboard',
    icon: 'icon-speedometer',    
  },
  {
    name: 'Order list',
    url: '/orderList',
    icon: 'icon-chart'
  },
  {
    divider: true
  },
  // {
  //   title: true,
  //   name: 'Do Intake'
  // },  
  {
    name: 'DO-Intake',
    icon: 'icon-pencil',
    url: '/doIntake',
    // badge: {
    //   variant: 'info',
    //   text: 'New'    
    // }
  },
  {
    name: 'Scheduler',
    icon: 'icon-puzzle',
    url: '/doIntake',
  },
  // {
  //   title: true,
  //   name: 'Dispatch'
  // },
  {
    name: 'Dispatch',
    icon: 'icon-puzzle',
    url: '/doIntake',
  },  
  {
    divider: true
  },
  // {
  //   title: true,
  //   name: 'Review',
  // },
  {
    name: 'Review',
    url: '/doIntake',
    icon: 'icon-star',   
  },
  {
    divider: true
  },
  // {
  //   title: true,
  //   name: 'Billing',
  // },
  {
    name: 'Invoice',
    url: '/doIntake',
    icon: 'icon-bell',   
  }
];
