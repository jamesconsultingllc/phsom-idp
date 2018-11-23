import { Component, OnInit } from "@angular/core";
import { Location, PopStateEvent } from "@angular/common";
import { filter } from "rxjs/operators";
import { Router, NavigationEnd, NavigationStart } from "@angular/router";
import PerfectScrollbar from "perfect-scrollbar";

@Component({
  selector: "app-admin-layout",
  templateUrl: "./admin-layout.component.html",
  styleUrls: ["./admin-layout.component.scss"]
})
export class AdminLayoutComponent implements OnInit {
  private lastPoppedUrl: string;
  private yScrollStack: number[] = [];

  constructor(public location: Location, private router: Router) {}

  ngOnInit() {
    const isWindows = navigator.platform.indexOf("Win") > -1;

    if (isWindows && !document.getElementsByTagName("body")[0].classList.contains("sidebar-mini")) {
      // if we are on windows OS we activate the perfectScrollbar function

      document.getElementsByTagName("body")[0].classList.add("perfect-scrollbar-on");
    } else {
      document.getElementsByTagName("body")[0].classList.remove("perfect-scrollbar-off");
    }
    const elemMainPanel = document.querySelector(".main-panel") as HTMLElement;
    const elemSidebar = document.querySelector(".sidebar .sidebar-wrapper") as HTMLElement;

    this.location.subscribe((ev: PopStateEvent) => {
      this.lastPoppedUrl = ev.url;
    });
    this.router.events.subscribe((event: any) => {
      if (event instanceof NavigationStart) {
        if (event.url !== this.lastPoppedUrl) {
          this.yScrollStack.push(window.scrollY);
        }
      } else if (event instanceof NavigationEnd) {
        if (event.url === this.lastPoppedUrl) {
          this.lastPoppedUrl = undefined;
          window.scrollTo(0, this.yScrollStack.pop());
        } else {
          window.scrollTo(0, 0);
        }
      }
    });
    this.router.events.pipe(filter(event => event instanceof NavigationEnd)).subscribe(
      () => {
        elemMainPanel.scrollTop = 0;
        elemSidebar.scrollTop = 0;
      });
    if (window.matchMedia(`(min-width: 960px)`).matches && !this.isMac()) {
      let ps = new PerfectScrollbar(elemMainPanel);
      ps = new PerfectScrollbar(elemSidebar);
    }
  }

  afterViewInit() {
    this.runOnRouteChange();
  }

  isMaps(path) {
    let title = this.location.prepareExternalUrl(this.location.path());
    title = title.slice(1);
    return !(path === title);
  }

  runOnRouteChange(): void {
    if (window.matchMedia(`(min-width: 960px)`).matches && !this.isMac()) {
      const elemMainPanel = document.querySelector(".main-panel") as HTMLElement;
      const ps = new PerfectScrollbar(elemMainPanel);
      ps.update();
    }
  }

  isMac(): boolean {
    let bool = false;
    if (navigator.platform.toUpperCase().indexOf("MAC") >= 0 || navigator.platform.toUpperCase().indexOf("IPAD") >= 0) {
      bool = true;
    }
    return bool;
  }

}