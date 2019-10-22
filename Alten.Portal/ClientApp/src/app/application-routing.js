"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ApplicationRoutes = [
    { path: '', loadChildren: '../app/vehicles/vehicles.modules#VehiclesModule', pathMatch: 'full' },
    {
        path: 'vehicles', loadChildren: '../app/vehicles/vehicles.modules#VehiclesModule'
    },
];
//# sourceMappingURL=application-routing.js.map